import { useParams } from 'react-router-dom'
import { useStateContext } from '../contexts/ContextProvider'
import { useEffect, useState } from 'react'
import axiosClient from '../axios-client'
import Table from 'react-bootstrap/Table'

const OrderedProductList = () => {
    let { id } = useParams()
    const [list, setList] = useState([])
    const { user, token, setNotification } = useStateContext()

    if (!token) {
        window.location.href = '/'
    }

    useEffect(() => {
        document.title = 'Megrendelt termékek | easyQuote'
        getList()
    }, [])

    const getList = () => {
        axiosClient.get(`/ordered_products/list/${id}`)
            .then(({ data }) => { setList(data.data) })
            .catch(error => console.error('Connection error.'))
    }

    function formatPrice(price) {
        return price.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ' ')
    }

    const onDelete = (list) => {
        axiosClient.delete(`/ordered_products/${list.id}`)
            .then(() => {
                setNotification('A tétel sikeresen törölve')
                getList()
            })
            .catch(error => console.error('Connection error.'))
    }

    return (
        <div className='orderedProductList'>
            <h1>Megrendelt termékek:</h1>
            <br />
            {list.length === 0 &&
                <h4>Jelenleg nincs megjeleníthető terméklista a rendeléshez.</h4>}
            {list.length > 0 &&
                <div className='table'>                    
                    <Table responsive bordered hover size="sm" >
                        <thead>
                            <tr>
                                <th>Terméknév</th>
                                <th>Mennyiség</th>
                                <th>Ár</th>
                                {user.employee == true && <th className='text-center'>Törlés</th>}
                            </tr>
                        </thead>
                        <tbody>
                            {list.map((list) => (
                                <tr key={list.id}>
                                    <td className='right'>{list.product_id.name}
                                        <a href={`/termekek/${list.product_id.id}`} target='_blank'>
                                            <i className='bx bx-link-external'></i>
                                        </a>
                                    </td>
                                    <td>{list.quantity}</td>
                                    <td>{formatPrice(list.price)} Ft</td>
                                    {user.employee == true && <td className='text-center'><i onClick={ev => onDelete(list)} className='bx bx-x-circle delete-icon'></i></td>}
                                </tr>
                            ))}
                            <tr>
                                <td colSpan={2}><strong>Összesen:</strong></td>
                                <td><strong>{formatPrice(list.reduce((total, item) => total + item.price, 0))} Ft</strong></td>
                                {user.employee == true && <td></td>}
                            </tr>
                        </tbody>
                    </Table>
                    <small>* az árak tájékoztató jellegűek, az árváltozás jogát fenntartjuk</small>
                </div>
            }</div>
    )
}

export default OrderedProductList