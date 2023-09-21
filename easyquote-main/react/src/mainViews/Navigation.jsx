import { useStateContext } from "../contexts/ContextProvider.jsx"
import { useEffect } from "react"
import { Navbar, Nav, Container } from 'react-bootstrap'
import axiosClient from '../axios-client.js'

const Navigation = () => {
    const { user, token, setUser, setToken } = useStateContext()

    const onLogout = (ev) => {
        ev.preventDefault()
        axiosClient.post('/logout')
            .then(() => {
                setUser({})
                setToken(null)
            })
            .catch(error => console.error(error))
    }

    useEffect(() => {
        if (token) {
            axiosClient.get('/user')
                .then(({ data }) => { setUser(data) })
                .catch(error => console.error('Connection error'))
        }
    }, [])

    return (
        <Navbar id="navbar" sticky="top" bg="light" expand="lg">
            <Container>
                <Navbar.Brand id="brand" href="/">easyQuote</Navbar.Brand>
                <Navbar.Toggle aria-controls="basic-navbar-nav" />
                <Navbar.Collapse id="basic-navbar-nav">
                    <Nav className="navright">
                        {user.employee == true && <>
                            <Nav.Link href="/termekek"><i className='bx bx-store nav-icon'></i>&nbsp;Termékek</Nav.Link>
                        </>}
                        {token && <>
                            <Nav.Link href="/rendelesek"><i className='bx bx-list-ul nav-icon'></i>&nbsp;Rendelések</Nav.Link>
                            <Nav.Link href={`/profil/${user.id}`}><i className='bx bxs-user nav-icon'></i>&nbsp;Adataim</Nav.Link>
                            <Nav.Link onClick={onLogout}><i className='bx bx-log-out nav-icon'></i>&nbsp;Kijelentkezés</Nav.Link>
                        </>}
                        {!token && <Nav.Link href="/regisztracio"><i className='bx bxs-user-plus nav-icon-plus'></i>&nbsp;Regisztráció&nbsp;</Nav.Link>}
                        {!token && <Nav.Link href="/bejelentkezes"><i className='bx bxs-user nav-icon'></i>&nbsp;Bejelentkezés</Nav.Link>}
                    </Nav>
                </Navbar.Collapse>
            </Container>
        </Navbar>
    )
}

export default Navigation