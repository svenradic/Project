import React from 'react'

export default function FormCreate({ setStocks, stock, setStock, editIndex, setEditIndex}) {

  function closeEdit(){
    setEditIndex(-1)
  }

  function editStocks(){
    setStocks((previousStocks) => {
      const updatedStocks = [...previousStocks];
      return updatedStocks.map((s, i) => {
        return i === editIndex ? stock : s;
      });
    })
    setEditIndex(-1);
  }
 
  function handleInputChange(event) {
    const { name, value } = event.target;
    setStock({ ...stock, [name]: value });
  };

  return (
    <div className={editIndex >= 0 ? '' : 'hidden'} id="divEdit">
    <h2>Edit Stock</h2>
      <form className="form" id="formEdit" onSubmit={editStocks}>
        <div className="form-group">
          <label for="name">Symbol</label>
        <input type="text" id="symbolEdit" name="symbol" value={stock.symbol || '' } onChange={handleInputChange}/>
        </div>
        
        <div className="form-group">
          <label for="name">Company</label>
        <input type="text" id="companyEdit" name="companyName" value={stock.companyName || ''} onChange={handleInputChange} /> 
        </div>
    
        <div className="form-group">
          <label for="message">Market Cap</label>
          <input type="number" id="marketCapEdit" name="marketCap" value={stock.marketCap || ''} onChange={handleInputChange} /> 
        </div>

        <div className="form-group">
          <label for="message">Current Price</label>
          <input type="number" id="currentPriceEdit" name="currentPrice" value={stock.currentPrice || ''} onChange={handleInputChange} /> 
        </div>
        
        <button className="submit-button" type="submit">Submit</button>
        <button onClick={closeEdit} className="close-button" id="close-button" type="button">Close</button>
      </form>
   </div>
  )
}