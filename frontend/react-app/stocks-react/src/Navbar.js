import React, {useContext} from 'react'
import './App.css';
import { Link } from 'react-router-dom'
import { UserContext } from './UserProvider';

function Navbar() {
  const { getUser, logout } = useContext(UserContext);

  return (
    <nav>
      <div className="navbar">
        <div className="logo"><Link to="/">
            Stocks
          </Link></div>
        <div className="nav-links">
          <Link to="/Services">
            Services
          </Link>
          <Link to="/AboutUs">
            About
          </Link>
          <Link to="/ContactUs">
            Contact
          </Link>
          {
            getUser() ? 
            <a onClick={() => logout()}>Logout</a>: 
            <Link to="/Login">
              Login
            </Link> 
          }
          
          
        </div>
      </div>
  </nav>
  )
}

export default Navbar
