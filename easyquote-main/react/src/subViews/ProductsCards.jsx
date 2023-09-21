import { useState, useEffect } from 'react'
import { useStateContext } from '../contexts/ContextProvider.jsx'
import axiosClient from '../axios-client.js'
import Modal from 'react-modal'

const ProductsCards = () => {
    const [products, setProducts] = useState([])
    const [openOrders, setOpenOrders] = useState([])
    const [selectedProduct, setSelectedProduct] = useState(null)
    const [searchText, setSearchText] = useState('')
    const [selectedOption, setSelectedOption] = useState('option1')
    const { user, token, setNotification } = useStateContext()
    const [error, setError] = useState(null)
    const [selectedOrderId, setSelectedOrderId] = useState(null)
    const [productQuantities, setProductQuantities] = useState({})

    if (!token || user.employee == false) {
        window.location.href = '/'
    }

    useEffect(() => {
        document.title = 'Termékek | easyQuote'
        getOpenOrders()
        getProducts()
    }, [])

    const getOpenOrders = () => {
        axiosClient.get(`/openorders`)
            .then(({ data }) => { setOpenOrders(data.data) })
            .catch(error => console.error('Connection error.'))
    }

    const getProducts = () => {
        axiosClient.get(`/products`)
            .then(({ data }) => { setProducts(data.data) })
            .catch(error => console.error('Connection error.'))
    }

    const getByName = (productName) => {
        axiosClient.get(`/products/name/${productName}`)
            .then(({ data }) => { setProducts(data.data) })
            .catch(error => console.error('Connection error.'))
    }

    const getByCategory = (productCategory) => {
        axiosClient.get(`/products/category/${productCategory}`)
            .then(({ data }) => { setProducts(data.data) })
            .catch(error => console.error('Connection error.'))
    }

    const getBySubCategory = (productSubCategory) => {
        axiosClient.get(`/products/subCategory/${productSubCategory}`)
            .then(({ data }) => { setProducts(data.data) })
            .catch(error => console.error('Connection error.'))
    }

    const handleSelectChange = (event) => {
        const selectedId = event.target.value
        setSelectedOrderId(selectedId)
    }

    const handleSearch = (e) => {
        e.preventDefault()
        if (searchText != '') {
            if (selectedOption === 'option1') {
                getByName(searchText)
            } else if (selectedOption === 'option2') {
                getByCategory(searchText)
            } else if (selectedOption === 'option3') {
                getBySubCategory(searchText)
            }
        }
        else {
            getProducts()
        }
    }

    const handleResetSearch = () => {
        getProducts()
        setSearchText('')
    }

    function formatPrice(price) {
        return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ' ')
    }

    const addToCart = (productId, preQuantity, productPrice) => {

        if (selectedOrderId === null) {
            setError("Nincs rendelés kiválasztva!")
        }
        else {
            setError(null)
            const payload = {
                order_id: selectedOrderId,
                product_id: productId,
                quantity: preQuantity,
                price: preQuantity * productPrice,
            }
            axiosClient.post('/ordered_products', payload)
                .then(() => { setNotification('A termék sikeresen hozzáadva!') })
                .catch(error => console.error('Connection error.'))
        }
    }

    const openModal = (productId) => {
        axiosClient.get(`/products/${productId}`)
            .then(data => setSelectedProduct(data.data))
            .catch(error => console.error('Connection error.'))
    }

    const closeModal = () => {
        setSelectedProduct(null)
    }

    const customStyles = {
        content: {
            top: '50%',
            left: '50%',
            right: 'auto',
            bottom: 'auto',
            fontSize: '19px',
            maxWidth: '1000px',
            marginRight: '-40%',
            color: 'rgb(97, 96, 96)',
            transform: 'translate(-50%, -50%)',
        },
    }

    return (
        <>
            <div className='select-order'>
                <label htmlFor='select'>Módosítani kívánt rendelés: </label>
                <select id='select' onChange={handleSelectChange}>
                    <option id="empty" value="" disabled selected >Válasszon...</option>
                    {openOrders.map(openorder => (
                        <option key={openorder.id} value={openorder.id}>{openorder.user.email} - {openorder.address}</option>
                    ))}
                </select>
            </div>
            {error && <p style={{ color: "red", fontWeight: "bold" }}>{error}</p>}
            <div className='search-bar'>
                <form onSubmit={handleSearch}>
                    <label htmlFor="select">Keresés: </label>
                    <select id='select' value={selectedOption} onChange={e => setSelectedOption(e.target.value)}>
                        <option className='option' value="option1">név szerint</option>
                        <option className='option' value="option2">kategória szerint</option>
                        <option className='option' value="option3">alkategória szerint</option>
                    </select>
                    <input
                        type="text"
                        placeholder='keresés...'
                        value={searchText}
                        onChange={e => setSearchText(e.target.value)}
                    />
                    <button type="submit" className="btn btn-danger btn-sm"><i className='bx bx-search'></i></button>&nbsp;
                    <button type='button' className="btn btn-danger btn-sm" onClick={handleResetSearch}><i className='bx bx-reset'></i></button>
                </form>
            </div>
            {products.length === 0 && <p>Nincs megjeleníthető termék.</p>}
            <div className="row">
                {products.map(product => (
                    <div className="col-lg-2 col-sm-4 gy-3" key={product.id}>
                        <div className="card">
                            <img className="card-img-top" src={`http://localhost:8000/images/${product.img_url}`} alt="Termék kép"></img>
                            <div className="card-body">
                                <h6 className="card-title">{product.name.length > 40 ? product.name.substring(0, 40) + '...' : product.name}</h6>
                                <p className="card-text text-black description">{product.description.length > 50 ? product.description.substring(0, 50) + '...' : product.description}</p>
                                <p className="card-text price">{formatPrice(product.price)} Ft</p>
                            </div>
                            <input
                                type="number"
                                min="1"
                                max="100"
                                value={productQuantities[product.id] || 1}
                                onChange={(e) => {
                                    const newValue = Math.max(parseInt(e.target.value, 10), 1)
                                    setProductQuantities(prevState => ({
                                        ...prevState,
                                        [product.id]: newValue,
                                    }))
                                }}
                            />
                            <button className="btn btn-danger btn-sm add" onClick={() => addToCart(product.id, productQuantities[product.id] || 1, product.price)}>Hozzáadás</button>
                            <button className="btn btn-light btn-sm details" onClick={() => openModal(product.id)}>Részletek</button>
                        </div>
                    </div>
                ))}
                <Modal style={customStyles} isOpen={selectedProduct !== null} onRequestClose={closeModal} appElement={document.getElementById('root')}>
                    {selectedProduct && (
                        <div>
                            <div style={{ display: 'flex', alignItems: 'center' }}>
                                <h2 style={{ flex: 1 }}>{selectedProduct.name}</h2>
                                <a href={`/termekek/${selectedProduct.id}`} target='_blank' style={{ fontSize: '25px', color: 'rgb(97, 96, 96)' }}>
                                    <i className='bx bx-link-external'></i>
                                </a>
                            </div>
                            <img className="w-25" src={`http://localhost:8000/images/${selectedProduct.img_url}`} alt="Termék kép"></img>
                            <p><b>Kategória: </b> {selectedProduct.category}</p>
                            <p><b>Alkategória: </b> {selectedProduct.sub_category}</p>
                            <p><b>Leírás:</b> {selectedProduct.description}</p>
                            <p><b>{formatPrice(selectedProduct.price)} Ft</b></p>
                            <button className="btn btn-danger btn-sm" onClick={closeModal}>Bezárás</button>
                        </div>
                    )}
                </Modal>
            </div>
        </>
    )
}

export default ProductsCards