import React from 'react'
import { Link } from 'react-router-dom'

export default function Stock({symbol, companyName, currentPrice, marketCap, index, deleteItem, user}) {
  return (
    <tr>
      <td>{symbol}</td>
      <td>{companyName}</td>
      <td>{currentPrice}</td>
      <td>{marketCap}</td>
      {
           user.role === 'admin' ? 
           <td>
            <Link to={`/Edit/${index}`}>Edit</Link>
            <button onClick={() => deleteItem(index)}>Delete</button>
           </td>:
           null
        }
    </tr>
  )
}

