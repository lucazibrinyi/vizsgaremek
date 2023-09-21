import { useEffect, useState } from 'react'
import { useStateContext } from '../contexts/ContextProvider'
import axiosClient from '../axios-client'
import Table from 'react-bootstrap/Table'

const Orders = () => {
    const [orders, setOrders] = useState([])
    const { user, token } = useStateContext()

    if (!token) {
        window.location.href = '/'
    }

    useEffect(() => {
        document.title = 'Rendelések | easyQuote'
        getOrders()
    }, [])

    const getOrders = () => {
        axiosClient.get('/orders')
            .then(({ data }) => { setOrders(data.data) })
            .catch(error => console.error('Connection error.'))
    }

    return (
        <div className='orders'>
            <div className="orders-header">
                <h1>Rendelések:</h1>
                {user.employee == false && <><a id='get-new-quote-btn' href="/ujrendeles" >Új ajánlatkérés</a></>}
            </div>
            <br />
            {orders.length === 0 && <p>Nincs megjeleníthető rendelés.</p>}
            {orders.map((orders) => (
                <Table responsive bordered hover size="sm" key={orders.id}>
                    <tbody>
                        <tr>
                            <th>Felhasználó: </th>
                            <td>{orders.user.email}</td>
                        </tr>
                        <tr>
                            <th>IRSZ:</th>
                            <td>{orders.postal_code}</td>
                        </tr>
                        <tr>
                            <th>Város:</th>
                            <td>{orders.city}</td>
                        </tr>
                        <tr>
                            <th>Cím:</th>
                            <td>{orders.address}</td>
                        </tr>
                        <tr>
                            <th>Telefonszám:</th>
                            <td>{orders.phone_number}</td>
                        </tr>
                        <tr>
                            <th>Fizetési határidő:</th>
                            <td>{orders.payment_deadline}</td>
                        </tr>
                        <tr>
                            <th>Felmérés:</th>
                            <td>{orders.survey ? <>Elvégezve</> : <>-</>}</td>
                        </tr>
                        <tr>
                            <th>Teljesítve:</th>
                            <td>{orders.completed ? <>Igen</> : <>-</>}</td>
                        </tr>
                        <tr>
                            <th>Létrehozva:</th>
                            <td>{orders.created_at}</td>
                        </tr>
                        <tr>
                            <th>Módosítva:</th>
                            <td>{orders.updated_at}</td>
                        </tr>
                        <tr>
                            <th>Árkalkuláció:</th>
                            <td>
                                <a href={`/rendelesek/${orders.id}`} >
                                    <i className='bx bx-link'></i>
                                </a>
                            </td>
                        </tr>
                    </tbody>
                </Table>
            ))}
        </div>
    )
}

export default Orders