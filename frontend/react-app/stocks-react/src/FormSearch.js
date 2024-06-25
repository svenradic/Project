import React from 'react'
import { filterStock } from './services';

export default function FormSearch({setIsFiltering, setFilteredStocks}) {
  const [stockSearch, setStockSearch] = React.useState({});

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setStockSearch({ ...stockSearch, [name]: value });
  };

  async function searchStock(e){
    e.preventDefault();
    const response = await filterStock(stockSearch);
    setFilteredStocks(response);
    setIsFiltering(true);
  }
  const stockProperties = ['Symbol', 'CompanyName', 'MarketCap', 'CurrentPrice'];
  return (
    <div>
      <h2> Search Stocks</h2>
      <form className="form" id="form_search" onSubmit={searchStock}>
        <div className="form-group">
          <label htmlFor="name">Symbol</label>
        <input type="text" id="symbol" name="symbolQuery" onChange={handleInputChange}/>
        </div>
        
        <div className="form-group">
          <label htmlFor="name">Company</label>
        <input type="text" id="company" name="companyQuery" onChange={handleInputChange}/> 
        </div>
    
        <div className="form-group">
          <label htmlFor="message">Minimum Market Cap</label>
          <input type="number" id="minMarketCap" name="minMarketCap" onChange={handleInputChange} /> 
        </div>
        <div className="form-group">
          <label htmlFor="message">Maximum Market Cap</label>
          <input type="number" id="maxMarketCap" name="maxMarketCap" onChange={handleInputChange}/> 
        </div>
        <div className="form-group">
          <label htmlFor="message">Minimum Current Price</label>
          <input type="number" id="minCurrentPrice" name="minCurrentPrice" onChange={handleInputChange}/> 
        </div>
        <div className="form-group">
          <label htmlFor="message">Maximum Current Price</label>
          <input type="number" id="maxCurrentPrice" name="maxCurrentPrice" onChange={handleInputChange}/> 
        </div>
        <div className="form-group">
          <label htmlFor="message">Order By</label>
          <select id="selectProperty" name="orderBy" onChange={handleInputChange}>
            <option value="">Select Property</option>
            {stockProperties.map((property, index) => (
              <option key={index} value={property}>{property}</option>
            ))}
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="message">Sort Order</label>
          <select id="selectProperty" name="sortOrder" onChange={handleInputChange}>
            <option value="">Select sort order</option>
              <option key={1} value={'DESC'}>DESC</option>
              <option key={2} value={'ASC'}>ASC</option>
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="message">RPP</label>
          <input type="number" id="rpp" name="rpp" onChange={handleInputChange}/> 
        </div>
        <div className="form-group">
          <label htmlFor="message">Page Number</label>
          <input type="number" id="page" name="pageNumber" onChange={handleInputChange}/> 
        </div>
        
        <button className="submit-button" type="submit">Submit</button>
      </form>
    </div>
  )
}