import axios from 'axios'

const stockClient = axios.create({
  baseURL: 'https://localhost:7095/Stock/stocks',
  headers: {
    'Content-Type': 'application/json'
  },
});

export async function getStocks(){
  try{
    const response = stockClient.get('');
    return (await response).data;
  }
  catch(error){
    console.error('Error fetching stocks:', error);
    throw error;
  }
};

export async function postStock(stock){
  try{
    const response = stockClient.post('', stock);
    return (await response).data;
  }
  catch(error){
    console.error('Error posting stock:', error);
    throw error;
  }
}

export async function deleteStock(id){
  try{
    const response = stockClient.delete(`/${id}`);
    return (await response).data;
  } 
  catch(error){
    console.error('Error posting stock:', error);
    throw error;
  }
}

export async function updateStock(id, stock){
  try{
    const response = stockClient.put(`/${id}`, stock);
    return (await response).data;
  } 
  catch(error){
    console.error('Error posting stock:', error);
    throw error;
  }
}

export async function getStock(id){
  try{
    const response = stockClient.get(`?StockId=${id}`);
    return (await response).data;
  }
  catch(error){

  }
}