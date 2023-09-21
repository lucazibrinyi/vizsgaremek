import { useEffect, useState } from "react"
import { useStateContext } from "../contexts/ContextProvider.jsx"
import axiosClient from "../axios-client.js"

const User = () => {
    const [error, setError] = useState(null)
    const { token, setNotification } = useStateContext()
    const [user, setUser] = useState({
        id: null,
        name: '',
        email: '',
        password: '',
        password_confirmation: '',
    })

    if (!token) {
        window.location.href = '/'
    }

    useEffect(() => {
        document.title = 'Profil | easyQuote'
        getUser()
    }, [])

    const getUser = () => {
        axiosClient.get('/user')
            .then(({ data }) => { setUser(data) })
            .catch(error => console.error('Connection error.'))
    }

    const onSubmit = ev => {
        ev.preventDefault()
        if (user.name.trim() === "" || user.email.trim() === "") {
            setError({ field: ['Töltse ki a kötelező mezőket!'] })
        }
        else if (user.password !== user.password_confirmation) {
            setError({ field: ['A jelszómezők nem egyeznek!'] })
        }
        else {
            axiosClient.put(`/users/${user.id}`, user)
                .then(() => { setNotification('A felhasználói adatok sikeresen frissítve!') })
                .catch(err => {
                    const response = err.response
                    if (response && response.status === 422) {
                        console.error('Connection error.')
                    }
                })
        }
    }

    return (
        <div className="login-signup-form">
            <form className="form" onSubmit={onSubmit} >
                <h1>Adatok megváltoztatása:</h1>
                <br />
                <div>
                    <label htmlFor='name'>Felhasználónév:</label><br />
                    <input
                        id='name'
                        type="text"
                        value={user.name}
                        onChange={(ev) => setUser({ ...user, name: ev.target.value })}
                        required
                    />
                </div>
                <div>
                    <label htmlFor='email'>E-mail cím:</label><br />
                    <input
                        id='email'
                        type="email"
                        value={user.email}
                        onChange={(ev) => setUser({ ...user, email: ev.target.value })}
                        required
                    />
                </div>
                <div>
                    <label htmlFor='password'>Új jelszó:</label><br />
                    <input
                        id='password'
                        type="password"
                        name="password"
                        autoComplete="current-password"
                        onChange={(ev) => setUser({ ...user, password: ev.target.value })}
                        required
                    />
                </div>
                <div>
                    <label htmlFor='password-repeat'>Új jelszó megerősítése:</label><br />
                    <input
                        id='password-repeat'
                        type="password"
                        name="password"
                        autoComplete="current-password"
                        onChange={(ev) => setUser({ ...user, password_confirmation: ev.target.value })}
                        required
                    />
                </div>
                <br />
                <button type="submit" onClick={onSubmit}>Mentés</button><br />
                {error && <p style={{ color: "red", fontWeight: "bold" }}>
                    {Object.keys(error).map(key => (
                        <p key={key}>{error[key][0]}</p>
                    ))}
                </p>}
            </form>
        </div>
    )
}

export default User