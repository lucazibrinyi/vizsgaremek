import { useStateContext } from "../contexts/ContextProvider.jsx";
import { Outlet } from "react-router-dom"
import Navigation from './Navigation.jsx'
import Footer from './Footer.jsx'

const StandardView = () => {
    const { notification } = useStateContext()

    return (
        <div>
            <Navigation />
            <div id='bar'></div>
            <div className="container main">
                <Outlet />
            </div>
            {notification &&
                <div className="notification">
                    {notification}
                </div>
            }
            <Footer />
        </div>
    )
}

export default StandardView