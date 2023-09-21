import { RouterProvider } from 'react-router-dom'
import { ContextProvider } from './contexts/ContextProvider'
import ReactDOM from 'react-dom/client'
import router from "./router.jsx"
import React from 'react'
import './index.css'
import '../node_modules/bootstrap/dist/css/bootstrap.min.css'
import '../node_modules/bootstrap/dist/js/bootstrap.min.js'
import '../node_modules/bootstrap-icons/font/bootstrap-icons.css'
import '../node_modules/boxicons/css/boxicons.min.css'

ReactDOM.createRoot(document.getElementById('root')).render(
    <React.StrictMode>
        <ContextProvider>
            <RouterProvider router={router} />
        </ContextProvider>
    </React.StrictMode>
)
