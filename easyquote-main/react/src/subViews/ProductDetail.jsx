import { useEffect, useState } from 'react'
import { useParams } from 'react-router-dom'
import { useStateContext } from '../contexts/ContextProvider'
import axiosClient from '../axios-client'

const ProductDetails = () => {
    let { id } = useParams()
    const { token, user } = useStateContext()
    const [product, setProduct] = useState({})

    if (!token) {
        window.location.href = '/'
    }

    useEffect(() => {
        document.title = 'Termékadatlap | easyQuote'
        getProduct(id)
    }, [])

    const getProduct = () => {
        axiosClient.get(`/products/${id}`)
            .then(data => setProduct(data.data))
            .catch(error => console.error('Connection error.'))
    }

    function formatPrice(price) {
        return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ' ')
    }

    return (
        <div className='product'>
            {!product.name ?
                <h2 id='none'>Nincs megjeleníthető termék</h2> :
                <>
                    <h2><b></b>{product.name}</h2>
                    <img className="w-25" src={`http://localhost:8000/images/${product.img_url}`} alt="Termék kép"></img>
                    <p><b>Kategória: </b>{product.category}</p>
                    <p><b>Alkategória: </b>{product.sub_category}</p>
                    <p><b>Leírás:</b> {product.description}</p>
                    {user.employee == true && <h4><b>{formatPrice(product.price)} Ft</b></h4>}
                </>
            }
        </div>
    )
}

export default ProductDetails