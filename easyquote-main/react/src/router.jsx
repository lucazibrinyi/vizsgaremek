import { createBrowserRouter } from 'react-router-dom'
import User from './subViews/User'
import HowTo from './subViews/HowTo'
import Orders from './subViews/Orders'
import Welcome from './subViews/Welcome'
import LogInForm from './subViews/LogInForm'
import SignUpForm from './subViews/SignUpForm'
import NotFound from './mainViews/NotFound.jsx'
import NewOrderForm from './subViews/NewOrderForm'
import StandardView from './mainViews/StandardView'
import ProductsCards from './subViews/ProductsCards'
import ProductDetails from './subViews/ProductDetail'
import OrderedProductList from './subViews/OrderedProductList'

const router = createBrowserRouter(
    [
        {
            path: '/',
            element: <StandardView />,
            children: [
                {
                    path: '/',
                    element: <Welcome />
                },
                {
                    path: '/ajanlat',
                    element: <HowTo />
                },
                {
                    path: '/regisztracio',
                    element: <SignUpForm />
                },
                {
                    path: '/bejelentkezes',
                    element: <LogInForm />
                },
                {
                    path: '/profil/:id',
                    element: <User key="userUpdate" />
                },
                {
                    path: '/termekek',
                    element: <ProductsCards />
                },
                {
                    path: '/termekek/:id',
                    element: <ProductDetails />
                },
                {
                    path: '/rendelesek',
                    element: <Orders />
                },
                {
                    path: '/rendelesek/:id',
                    element: <OrderedProductList />
                },
                {
                    path: '/ujrendeles',
                    element: <NewOrderForm />
                },
                {
                    path: '*',
                    element: <NotFound />
                },
            ]
        },
    ]
)

export default router