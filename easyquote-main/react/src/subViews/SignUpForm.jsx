import { useState, useEffect } from "react"
import { useStateContext } from "../contexts/ContextProvider"
import axiosClient from "../axios-client"

const SignUpForm = () => {
    const [username, setUsername] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [passwordConfirmation, setPasswordConfirmation] = useState('')
    const [error, setError] = useState(null)
    const { token, setUser, setToken } = useStateContext()

    useEffect(() => {
        document.title = 'Regisztráció | easyQuote'
    }, [])

    if (token) {
        window.location.href = '/'
    }

    const onSubmit = (ev) => {
        ev.preventDefault()

        const payload = {
            name: username,
            email: email,
            password: password,
            password_confirmation: passwordConfirmation,
        }

        axiosClient.post('/signup', payload)
            .then(({ data }) => {
                setUser(data.user)
                setToken(data.token)
                window.location.href = '/'
            })
            .catch(err => {
                const response = err.response
                if (response && response.status === 422) {
                    setError({ field: ['A mezők kitöltése helytelen!'] })
                }
            })
    }

    return (
        <div className="login-signup-form">
            <form onSubmit={onSubmit}>
                <h1>Regisztráció</h1>
                <br />
                <div>
                    <label htmlFor="name">Felhasználónév:*</label><br />
                    <input
                        id="name"
                        type="text"                        
                        value={username}
                        onChange={(e) => setUsername(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="email">E-mail cím:*</label><br />
                    <input
                        id="email"
                        type="email"
                        autoComplete='username'
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="password">Jelszó:* **</label><br />
                    <input
                        id="password"
                        type="password"
                        autoComplete='new-password'
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label htmlFor="passwordRepeat">Jelszó megerősítése:*</label><br />
                    <input
                        id="passwordRepeat"
                        type="password"
                        autoComplete='new-password'
                        value={passwordConfirmation}
                        onChange={(e) => setPasswordConfirmation(e.target.value)}
                        required
                    />
                </div>
                <small>* A csillaggal jelölt mezők kitöltése kötelező!</small><br />
                <small>** Min. 8 karakter, min. egy speciális karakter</small>
                <br /><br />
                <button type="submit" onClick={onSubmit}>Regisztráció</button>
                <p className="message">Már regisztrált? <a href="/bejelentkezes">Jelentkezzen be!</a></p>
                {error && <p style={{ color: "red", fontWeight: "bold" }}>
                    {Object.keys(error).map(key => (
                        <p key={key}>{error[key][0]}</p>
                    ))}
                </p>}
            </form>
        </div>
    )
}

export default SignUpForm