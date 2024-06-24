import React from 'react'

export default function Stock({symbol, companyName, currentPrice, marketCap, index, editStock, deleteStock}) {
  return (
    <tr>
      <td>{symbol}</td>
      <td>{companyName}</td>
      <td>{currentPrice}</td>
      <td>{marketCap}</td>
      <td>
          <button onClick={() => editStock(index)}>Edit</button>
          <button onClick={() => deleteStock(index)}>Delete</button>
        </td>
    </tr>
  )
}

