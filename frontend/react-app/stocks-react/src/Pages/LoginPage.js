import userEvent from '@testing-library/user-event';
import React, { useContext, useState } from 'react';
import { UserContext } from '../UserProvider';
import '../login.css'; // Import your CSS file for classNames
import { useNavigate } from 'react-router-dom';

function LoginPage() {
  const { login } = useContext(UserContext);
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState('');
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    // Pass the username, password, and role to the login function from context
    login(username, password, role);
    // Clear form fields after login
    setUsername('');
    setPassword('');
    setRole('');
    navigate('/');
  };
  return (
    <div className="loginFormContainer">
      <h2>Login</h2>
      <form onSubmit={handleSubmit} className="loginForm">
        <div className="formGroup">
          <label htmlFor="username">Username:</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="formGroup">
          <label htmlFor="role">Role:</label>
          <input
            type="text"
            id="role"
            value={role}
            onChange={(e) => setRole(e.target.value)}
            required
          />
        </div>
        <button type="submit" className="submitButton">Login</button>
      </form>
    </div>
  );
}

export default LoginPage
