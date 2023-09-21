import { useEffect } from "react"

const NotFound = () => {

    useEffect(() => {
        document.title = '404 | easyQuote'
    }, [])

    return (
        <div className="notfound">
            <h1>Error - 404</h1><br />
            <h2>A kért oldal nem található</h2><br />
            <a href="/">Vissza főoldalra</a>
        </div>
    )
}

export default NotFound