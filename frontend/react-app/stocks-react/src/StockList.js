import React from 'react';
import './App.css';
import Stock from './Stock.js';
import FormSearch from './FormSearch.js';
import {getStocks, deleteStock, getStock} from './services.js';
import { Link } from 'react-router-dom';
import DeleteConfimationModal from './DeleteConfimationModal.js';
import { useContext } from 'react';
import { UserContext } from './UserProvider.js';

function StockList() {
  const {getUser} = useContext(UserContext);
  const [stocks, setStocks] = React.useState([]);
  const [filteredStocks, setFilteredStocks] = React.useState([]);
  const [isFiltering, setIsFiltering] = React.useState(false);
  const [showModal, setShowModal] = React.useState(false);
  const [idDelete, setIdDelete] = React.useState(null);
  
  const user = getUser() || {};

  
  React.useEffect(() => {
    const fetchData = async () => {
      try {
        const data = await getStocks();
        setStocks(data);
      } catch (error) {
        console.error('Error fetching stocks:', error);
      }
    };
    fetchData();
  }, []);

 function handleDeleteModal(index){
    setShowModal(true);
    setIdDelete(index)
  };
  function handleCloseModal(){
    setShowModal(false);
    setIdDelete(null);
  }

  async function deleteItem(index){
    console.log(`Deleting item: ${index}`);
    const response = await deleteStock(index);
    const data = await getStocks();
    setStocks(data);
    setFilteredStocks(data);
    setShowModal(false);
  }  


  return (
    <main>
       <div>
        <FormSearch setFilteredStocks={setFilteredStocks} setIsFiltering={setIsFiltering}/>
        {
          user.role === 'admin' ? 
          <Link to='/Create'>Add Stock</Link>:
          null
        }
       </div>
      <table>
    <thead>
      <tr>
        <th>Symbol</th>
        <th>Company Name</th>
        <th>Current Price $</th>
        <th>Market Cap $</th>
        {
           user.role === 'admin' ? 
           <th>Actions</th>:
           null
        }
      </tr>
    </thead>
    <tbody id="stocks">
    {filteredStocks.length > 0 ?

     filteredStocks.map((stock, index) => { return <Stock key={index} symbol={stock.symbol} companyName={stock.companyName} currentPrice={stock.currentPrice} marketCap={stock.marketCap} index={stock.id} deleteItem
    ={deleteItem}></Stock>}) : 
  
    stocks.map((stock, index) => { return <Stock key={index} symbol={stock.symbol} companyName={stock.companyName} currentPrice={stock.currentPrice} marketCap={stock.marketCap} index={stock.id} deleteItem
    ={handleDeleteModal} user={user}></Stock>})}
    </tbody>
  </table>
  <DeleteConfimationModal
        show={showModal}
        handleClose={handleCloseModal}
        handleDelete={deleteItem}
        index={idDelete}
      />
    </main>
  );
}

export default StockList;