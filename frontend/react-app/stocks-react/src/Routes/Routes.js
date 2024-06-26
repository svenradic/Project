import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import Home from "../Pages/Home";
import Services from '../Pages/Services';
import AboutUs from '../Pages/AboutUs';
import ContactUs from '../Pages/ContactUs';
import FormCreate from "../FormCreate";
import FormEdit from "../FormEdit";
import LoginPage from "../Pages/LoginPage";


export const router = createBrowserRouter([
  {
    path: "/",
    element: <App/>,
    children: [
      {path: "", element: <Home/>},
      {path: "Services", element: <Services/>},
      {path: "AboutUs", element:<AboutUs/>},
      {path: "ContactUs", element:<ContactUs/>},
      {
        path: "/Create", 
        element: <FormCreate/>,
      },
      {
        path: "/Edit/:id",
        element: <FormEdit/>
      },
      {
        path:"/Login",
        element: <LoginPage/>
      }
    ]
  }
]);