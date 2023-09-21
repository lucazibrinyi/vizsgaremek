import { useState, useEffect } from 'react'
import { useStateContext } from '../contexts/ContextProvider'
import axiosClient from '../axios-client'

const LogInForm = () => {
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState(null)
    const { token, setUser, setToken } = useStateContext()

    useEffect(() => {
        document.title = 'Bejelentkezés | easyQuote'
    }, [])

    if (token) {
        window.location.href = '/'
    }

    const onSubmit = (ev) => {
        ev.preventDefault()

        const payload = {
            email: email,
            password: password,
        }

        setError(null)
        axiosClient.post('/login', payload)
            .then(({ data }) => {
                setUser(data.user)
                setToken(data.token)
                window.location.href = '/'
            })
            .catch(err => {
                const response = err.response
                if (response) {
                    if (response.status === 422) {
                        setError({
                            email: ['A megadott e-mail cím, vagy jelszó helytelen!']
                        })
                    } else if (response.status === 401) {
                        setError({
                            email: ['A megadott e-mail cím, vagy jelszó helytelen!']
                        })
                    } else {
                        setError(null)
                    }
                }
            })
    }

    return (
        <div className="login-signup-form">
            <form onSubmit={onSubmit}>
                <h1>Bejelentkezés</h1>
                <br />
                <div>
                    <label htmlFor="email">E-mail cím:</label><br />
                    <input
                        id="email"
                        type="email"
                        autoComplete="username"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Jelszó:</label><br />
                    <input
                        id="password"
                        type="password"
                        autoComplete="current-password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <br />
                <button type="submit" onClick={onSubmit}>Bejelentkezés</button>
                <p className="message">Még nem regisztrált? <a href="/regisztracio">Regisztráljon!</a></p>
                {error && <p style={{ color: "red", fontWeight: "bold" }}>
                    {Object.keys(error).map(key => (
                        <p key={key}>{error[key][0]}</p>
                    ))}
                </p>}
            </form>
        </div>
    )
}

export default LogInForm