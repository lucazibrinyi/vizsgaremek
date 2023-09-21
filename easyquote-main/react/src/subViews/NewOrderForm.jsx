import { useEffect, useState } from 'react'
import { useStateContext } from '../contexts/ContextProvider'
import axiosClient from '../axios-client'

const NewOrderForm = () => {
    const [error, setError] = useState(null)
    const { token, user } = useStateContext()
    const [order, setOrder] = useState({
        user_id: '',
        postal_code: '',
        city: '',
        address: '',
        phone_number: '',
    })

    if (!token) {
        window.location.href = '/'
    }

    useEffect(() => {
        document.title = 'Új ajánlatkérés | easyQuote'
    }, [])

    useEffect(() => {
        setOrder(prevState => ({
            ...prevState,
            user_id: user.id,
        }))
    }, [user.id])

    const onSubmit = (ev) => {
        ev.preventDefault()
        if (
            (order.postal_code < 1000 || order.postal_code > 9999) ||
            (order.phone_number.length < 10 || order.phone_number.length > 11) ||
            !/^[0-9]+$/.test(order.phone_number)
        ) {
            setError({
                field: ['A mezők kitöltése helytelen!']
            })
        }
        else {
            const payload = {
                user_id: order.user_id,
                postal_code: order.postal_code,
                city: order.city,
                address: order.address,
                phone_number: order.phone_number
            }
            axiosClient.post('/orders', payload)
                .then(() => {
                    window.location.href = '/rendelesek'
                })
                .catch(err => {
                    const response = err.response
                    if (response && response.status === 422) {
                        setError({
                            field: ['A mezők kitöltése helytelen!']
                        })
                    } else {
                        setError(null)
                    }
                })
        }
    }

    return (
        <div className="new-order-form">
            <form onSubmit={onSubmit}>
                <h1>Új ajánlat igénylése:</h1>
                <br />
                <div>
                    <label>Irányítószám:*</label><br />
                    <input
                        type="number"
                        onChange={(ev) => setOrder({ ...order, postal_code: ev.target.value })}
                        required
                        min="1000"
                        max="9999"
                        placeholder='1234'
                    />
                </div>
                <div>
                    <label>Város:*</label><br />
                    <input
                        type="text"
                        value={order.city}
                        onChange={(ev) => setOrder({ ...order, city: ev.target.value })}
                        required
                        maxLength="100"
                        placeholder='Budapest'
                    />
                </div>
                <div>
                    <label>Cím:*</label><br />
                    <input
                        type="text"
                        onChange={(ev) => setOrder({ ...order, address: ev.target.value })}
                        required
                        maxLength="100"
                        placeholder='Riasztó utca 1/a'
                    />
                </div>
                <div>
                    <label>Telefonszám:*</label><br />
                    <input
                        type="tel"
                        onChange={(ev) => setOrder({ ...order, phone_number: ev.target.value })}
                        required
                        maxLength="11"
                        minLength="10"
                        pattern="[0-9]{10,11}"
                        placeholder='06701234567'
                    />
                </div>
                <small>A csillaggal jelölt mezők kitöltése kötelező!</small>
                <br /><br />
                <button type="submit" onClick={onSubmit}>Küldés</button>
                {error && <p style={{ color: "red", fontWeight: "bold" }}>
                    {Object.keys(error).map(key => (
                        <p key={key}>{error[key][0]}</p>
                    ))}
                </p>}
            </form>
        </div>
    )
}

export default NewOrderForm