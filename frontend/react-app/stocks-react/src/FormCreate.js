import React from 'react'
import { postStock, getStocks } from './services';

export default function FormCreate({stocks, setStocks}) {
  const [stockCreate, setStockCreate] = React.useState({});

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setStockCreate({ ...stockCreate, [name]: value });
  };
  async function createStock(){
    const response = postStock(stockCreate);
    const data = await getStocks();
    setStocks(data);
  }

  return (
    <div>
      <h2>Create Stock</h2>
      <form className="form" id="formCreate" onSubmit={createStock}>
        <div className="form-group">
          <label htmlFor="name">Symbol</label>
        <input type="text" id="symbolCreate" name="symbol" onChange={handleInputChange} required />
        </div>
        
        <div className="form-group">
          <label htmlFor="name">Company</label>
        <input type="text" id="companyCreate" name="companyName" onChange={handleInputChange} required /> 
        </div>
    
        <div className="form-group">
          <label htmlFor="message">Market Cap</label>
          <input type="number" id="marketCapCreate" name="marketCap" onChange={handleInputChange} required /> 
        </div>

        <div className="form-group">
          <label htmlFor="message">Current Price</label>
          <input type="number" id="currentPriceCreate" name="currentPrice" onChange={handleInputChange} required /> 
        </div>
        
        <button className="submit-button" type="submit">Submit</button>
      </form>
    </div>
  )
}