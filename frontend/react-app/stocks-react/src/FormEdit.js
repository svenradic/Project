import React from 'react'
import { updateStock } from './services';
import { getStock } from './services';
import './App.css';
import { useParams, useNavigate } from 'react-router-dom';

export default function FormCreate() {
  const [stockEdit, setStockEdit] = React.useState({});
  const { id } = useParams();
  const navigate = useNavigate();
  React.useEffect(() => {
    const fetchStock = async () => {
      try{
        const stock = await getStock(id);
        setStockEdit(stock[0]);
      }
      catch(error){
        console.error('Error fetching stocks:', error);
      }
    };
    fetchStock();
  }, []);

  function closeEdit(){
    navigate('/Services');
  }

 async function editStocks(e){
    e.preventDefault();
    const response = await updateStock(id, stockEdit);
    console.log(response);
    if(response){
      navigate('/Services');
    }
  }
 
  function handleInputChange(event) {
    const { name, value } = event.target;
    setStockEdit({ ...stockEdit, [name]: value });
  };

  return (
    <div>
    <h2>Edit Stock</h2>
      <form className="form" id="formEdit" onSubmit={editStocks}>
        <div className="form-group">
          <label htmlFor="name">Symbol</label>
        <input type="text" id="symbolEdit" name="symbol" value={stockEdit.symbol || '' } onChange={handleInputChange}/>
        </div>
        
        <div className="form-group">
          <label htmlFor="name">Company</label>
        <input type="text" id="companyEdit" name="companyName" value={stockEdit.companyName || ''} onChange={handleInputChange} /> 
        </div>
    
        <div className="form-group">
          <label htmlFor="message">Market Cap</label>
          <input type="number" id="marketCapEdit" name="marketCap" value={stockEdit.marketCap || ''} onChange={handleInputChange} /> 
        </div>

        <div className="form-group">
          <label htmlFor="message">Current Price</label>
          <input type="number" id="currentPriceEdit" name="currentPrice" value={stockEdit.currentPrice || ''} onChange={handleInputChange} /> 
        </div>
        
        <button className="submit-button" type="submit">Submit</button>
        <button onClick={closeEdit} className="close-button" id="close-button" type="button">Close</button>
      </form>
   </div>
  )
}