import React from 'react';
import './App.css';
import {Data} from './Data.js'
import Stock from './Stock.js';
import FormCreate from './FormCreate.js';
import FormEdit from './FormEdit.js';

function StockList() {
  const [stocks, setStocks] = React.useState([]);
  const [stock, setStock] = React.useState({id: "", symbol: "", companyName: "", currentPrice:"", marketCap: ""});
  const [editIndex, setEditIndex] = React.useState(-1);

  React.useEffect(() => {
    const storedData = JSON.parse(localStorage.getItem('stocks')) || [];

    if(storedData.length === 0){
      localStorage.setItem('stocks', JSON.stringify(Data));

      setStock(Data);
    }
    else{
      setStocks(storedData);
    }
  }, []);

  React.useEffect(() => {
    localStorage.setItem('stocks', JSON.stringify(stocks));
  }, [stocks]);

  
  function editStock(index){
    setStock(stocks[index]);
    setEditIndex(index);
  }

  function deleteStock(index){
    setStocks((previousStocks) => {
      const updatedStocks = [...previousStocks];
      updatedStocks.splice(index, 1);
      return updatedStocks;
    })
  }  



 
  return (
    <div>
      <table>
    <thead>
      <tr>
        <th>Symbol</th>
        <th>Company Name</th>
        <th>Current Price $</th>
        <th>Market Cap $</th>
        <th>Actions</th>
      </tr>
    </thead>
    <tbody id="stocks">
    {stocks.map((stock, index) => { return <Stock key={index} symbol={stock.symbol} companyName={stock.companyName} currentPrice={stock.currentPrice} marketCap={stock.marketCap} index={index} editStock={editStock} deleteStock
    ={deleteStock}></Stock>})}
    </tbody>
  </table>
  <FormCreate stocks={stocks} setStocks={setStocks}/>
  <FormEdit setStocks={setStocks} stock={stock} setStock={setStock} editIndex={editIndex} setEditIndex={setEditIndex}/>
    </div>
  );
}

export default StockList;