import React from 'react'

export default function Stock({symbol, companyName, currentPrice, marketCap, index, editStock, deleteItem}) {
  return (
    <tr>
      <td>{symbol}</td>
      <td>{companyName}</td>
      <td>{currentPrice}</td>
      <td>{marketCap}</td>
      <td>
          <button onClick={() => editStock(index)}>Edit</button>
          <button onClick={() => deleteItem(index)}>Delete</button>
        </td>
    </tr>
  )
}

