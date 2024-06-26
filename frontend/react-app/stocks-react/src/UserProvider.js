import React, { createContext, useState } from 'react';

export const UserContext = createContext();

export const UserProvider = ({ children }) => {
  const [user, setUser] = useState(null);

  const login = (username, password, role) => {
    const newUser = { username, role };
    setUser(newUser);
  };

  const getUser = () => {
    return user;
  }


  const logout = () => {
    setUser(null);
  };

  console.log(user);
  

  return (
    <UserContext.Provider value={{ user, login, logout, getUser }}>
      {children}
    </UserContext.Provider>
  );
};