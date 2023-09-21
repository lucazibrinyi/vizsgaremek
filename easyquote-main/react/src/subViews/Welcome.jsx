const Welcome = () => {

    return (
        <div id='welcome'>
            <div className="flex-container">
                <i className='bx bx-shield-quarter welcome-icons'>&nbsp;</i>
                <h2>Biztonsági rendszerek</h2>
            </div>
            <p>Üdvözöljük az <span className='welcomebrand'>easyQuote</span> weboldalán! Mi vagyunk az Ön biztonsága és védelme szakértői. Cégünk elkötelezett amellett, hogy magas minőségű kamerarendszerekkel és riasztóberendezésekkel segítsük ügyfeleinket otthonuk és vállalkozásuk biztonságának megőrzésében. Vállaljuk komplett riasztó és kameraberendezések teljes körű telepítését és karbantartását. Kérje <b><a id='quote' href="/ajanlat">ajánlatunkat!</a></b></p>
            <div className="flex-container">
                <i className='bx bxs-camera-home welcome-icons'>&nbsp;</i>
                <h2>Kamerák</h2>
            </div>
            <p>Kiemelkedő minőségű kameráink segítségével lehetősége lesz 24/7 figyelni és rögzíteni az eseményeket. Legyen szó otthonról vagy irodáról, kameráink éles képminőséget és éjszakai látást kínálnak, hogy mindig tudja, mi történik. Intelligens funkcióink, mint a mozgásérzékelés és a távoli hozzáférés, segítenek még hatékonyabb és kényelmesebb védelmet biztosítani. Fedezze fel kamerarendszereink széles választékát, amelyek alkalmazkodnak az Ön egyedi igényeihez, és biztonságban érezheti magát bárhol is legyen.</p>
            <div className="flex-container">
                <i className='bx bxs-bell welcome-icons'>&nbsp;</i>
                <h2>Riasztók</h2>
            </div>
            <p>Fejlett és megbízható riasztórendszereket forgalmazunk, amelyek védelmet nyújtanak a betörések, tűz és egyéb veszélyek ellen. Intelligens érzékelőink és távoli hozzáférhetőségük révén bármikor figyelheti és vezérelheti riasztórendszerét, akár távolról is. Rugalmas és testreszabható megoldásainkkal minden egyedi igényt kielégítünk. Válassza riasztórendszereinket, és aludjon nyugodtan éjszaka, tudván, hogy otthona és értékei védelmet élveznek.</p>
            <div className="flex-container">
                <i className='bx bx-notepad welcome-icons' >&nbsp;</i>
                <h2>Garancia</h2>
            </div>
            <p>Szolgáltatásaink magukba foglalják a konfigurációt, telepítést és a rendszerek karbantartását is. Minden telepítésre <b>2 év</b> teljes körű garanciát vállalunk. Szakértőink mindig rendelkezésére állnak, hogy válaszoljanak kérdéseire és tanácsot adjanak az optimális megoldások kiválasztásában.</p>
            <div id='quote-get'>
                <a href="/ajanlat">Ajánlatot kérek!</a>
            </div>
        </div>
    )
}

export default Welcome