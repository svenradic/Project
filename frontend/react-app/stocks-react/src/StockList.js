import React from 'react';
import './App.css';
import Stock from './Stock.js';
import FormCreate from './FormCreate.js';
import FormEdit from './FormEdit.js';
import FormSearch from './FormSearch.js';
import {getStocks, deleteStock, getStock} from './services.js';

function StockList() {
  const [stocks, setStocks] = React.useState([]);
  const [filteredStocks, setFilteredStocks] = React.useState([]);
  const [stock, setStock] = React.useState({id: "", symbol: "", companyName: "", currentPrice:"", marketCap: ""});
  const [editIndex, setEditIndex] = React.useState(-1);
  const [isFiltering, setIsFiltering] = React.useState(false)

  
  React.useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getStocks();
        setStocks(data); 
        setFilteredStocks(data);
      } catch (error) {
        console.error('Error fetching stocks:', error);
      }
    };
    if(!isFiltering){
      fetchData();
    }
  }, [isFiltering]);

  
  async function editStock(index){
    const stockEdit = await getStock(index);
    setStock(stockEdit[0]);
    setEditIndex(index);
  }

  async function deleteItem(index){
    const response = await deleteStock(index);
    const data = await getStocks();
    setStocks(data);
  }  


  return (
    <main>
       <div>
        <FormCreate stocks={stocks} setStocks={setStocks}/>
        <FormSearch setFilteredStocks={setFilteredStocks} setIsFiltering={setIsFiltering}/>
       </div>
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
    {filteredStocks.length > 0 ?

     filteredStocks.map((stock, index) => { return <Stock key={index} symbol={stock.symbol} companyName={stock.companyName} currentPrice={stock.currentPrice} marketCap={stock.marketCap} index={stock.id} editStock={editStock} deleteItem
    ={deleteItem}></Stock>}) : 
  
    stocks.map((stock, index) => { return <Stock key={index} symbol={stock.symbol} companyName={stock.companyName} currentPrice={stock.currentPrice} marketCap={stock.marketCap} index={stock.id} editStock={editStock} deleteItem
    ={deleteItem}></Stock>})}
    </tbody>
  </table>
  <FormEdit setStocks={setStocks} stock={stock} setStock={setStock} editIndex={editIndex} setEditIndex={setEditIndex}/>
    </main>
  );
}

export default StockList;