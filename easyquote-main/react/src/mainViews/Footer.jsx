const Footer = () => {
    return (
        <footer id="footer">
            <div className="container">
                <div className="row">
                    <div className="col-lg-6">
                        <div className="center">
                            <h4>Linkek</h4>
                            <div className='footer-links'>
                                <div className='flex-container-footer'><i className='bx bx-chevron-right' ></i><a href="/">Főoldal</a></div>
                                <div className='flex-container-footer'><i className='bx bx-chevron-right' ></i><a href="/ajanlat">Ajánlat</a></div>
                                <div className='flex-container-footer'><i className='bx bx-chevron-right' ></i><a href="/teszt">Rólunk</a></div>
                                <div className='flex-container-footer'><i className='bx bx-chevron-right' ></i><a href="#kapcsolat">Kapcsolat</a></div>
                            </div>
                        </div>
                    </div>
                    <div className="col-lg-6">
                        <div id='kapcsolat' >
                            <h4 id='brand-name' >easyQuote</h4>
                            <p>Magyarország</p>
                            <p>Budapest 1234</p>
                            <p>Elfelejtettem utca 5.</p>
                            <p><i className='bx bxs-phone'></i> +36 20 123 4567</p>
                            <p><i className='bx bxs-envelope'></i> info@easyquote.hu</p>
                        </div>
                    </div>
                </div>
            </div>
        </footer>
    )
}

export default Footer