import { useEffect } from "react"

const HowTo = () => {

    useEffect(() => {
        document.title = 'Ajánlatkérés  | easyQuote'
    }, [])

    return (
        <div id="how-to">
            <h2>Az ajánlatkérés menete:</h2>
            <ul>
                <li>Regisztráljon oldalunkra a <a href="/regisztracio" target="_blank">Regisztráció</a> linkre kattintva</li>
                <li>Kattintson a <a href="/rendelesek" target="_blank">Rendelések</a> menüpontra</li>
                <li>Kattintson az <a href="/ujrendeles" target="_blank">Új ajánlatkérés</a> gombra, majd töltse ki az űrlapot</li>
                <li>Kollégáink hamarosan felveszik önnel a kapcsolatot további adategyeztetésre</li>
                <li>A telefonos megbeszélés után a <a href="/rendelesek" target="_blank">Rendelések</a> menüpont alatt láthatja a rendelés részleteit</li>
            </ul>
        </div>
    )
}

export default HowTo