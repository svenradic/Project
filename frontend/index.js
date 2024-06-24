const seedData = 
  [
    {
      id: "3d5cc3d6-c35c-4c40-8ce8-ae5def279dc5",
      symbol: "AAPL",
      companyName: "APPLE",
      currentPrice: 111,
      marketCap: 123456,
      traderId: "8cfd39d0-1893-4832-91da-6eb3bde09a61"
    },
    {
      id: "a2c29fc3-7504-4462-8f93-7fa9d145e8de",
      symbol: "MSFT",
      companyName: "Microsoft",
      currentPrice: 27,
      marketCap: 98876161235,
      traderId: "8cfd39d0-1893-4832-91da-6eb3bde09a61"
    },
    {
      id: "41af0023-e9b8-47e8-b485-e7e251c98efa",
      symbol: "TSLA",
      companyName: "Tesla inc",
      currentPrice: 150,
      marketCap: 1242556,
      traderId: "8cfd39d0-1893-4832-91da-6eb3bde09a61"
    }
  ];
  if(!localStorage.getItem("stocks")){
    localStorage.setItem('stocks', JSON.stringify(seedData));
  }

  if(window.location.pathname.endsWith('services.html')){
    document.addEventListener('DOMContentLoaded', () => {

      displayStocks();

      let form_search = document.getElementById('form_search');
      form_search.addEventListener('submit', (event) => {
        event.preventDefault();

        const symbol = document.getElementById('symbol').value;
        const company = document.getElementById('company').value;
        const minMarketCap = parseFloat(document.getElementById('minMarketCap').value);
        const maxMarketCap = parseFloat(document.getElementById('maxMarketCap').value);
        const minCurrentPrice = parseFloat(document.getElementById('minCurrentPrice').value);
        const maxCurrentPrice = parseFloat(document.getElementById('maxCurrentPrice').value);

        filterStock(symbol, company, minMarketCap, maxMarketCap, minCurrentPrice, maxCurrentPrice);
      });

      const formCreate = document.getElementById('formCreate');
      console.log(formCreate);
      formCreate.addEventListener('submit', (event) => {

        const symbol = document.getElementById('symbolCreate').value;
        const company = document.getElementById('companyCreate').value;
        const currentPrice = parseFloat(document.getElementById('currentPriceCreate').value);
        const marketCap = parseFloat(document.getElementById('marketCapCreate').value);

        let stock = {
          symbol: symbol,
          companyName: company,
          currentPrice: currentPrice,
          marketCap: marketCap,
        };
        createStock(stock);
      });


      const closeButton = document.getElementById('close-button');
      closeButton.addEventListener('click', (event) => {
        const divEdit = document.getElementById('divEdit');
        divEdit.classList.add('hidden');
      })

      });
   }

   

  function filterStock(symbol, company, minMarketCap, maxMarketCap, minCurrentPrice, maxCurrentPrice){
    let stocks = JSON.parse(localStorage.getItem('stocks')) || [];
    
    let filteredStocks = stocks.filter(function(stock) {
      return (!symbol || stock.symbol.toLowerCase().includes(symbol.toLowerCase())) &&
             (!company || stock.companyName.toLowerCase().includes(company.toLowerCase())) &&
             (!minMarketCap || stock.marketCap >= minMarketCap) &&
             (!maxMarketCap || stock.marketCap <= maxMarketCap) &&
             (!minCurrentPrice || stock.currentPrice >= minCurrentPrice) &&
             (!maxCurrentPrice || stock.currentPrice <= maxCurrentPrice);
    })
    displayFilterStocks(filteredStocks);
  }

  function displayStocks() {
    let stocks = JSON.parse(localStorage.getItem('stocks')) || [];
    let tableBody = document.getElementById('stocks') || null;
    if(stocks.length === 0 || tableBody === null){
      return
    }
    console.log(stocks);
    tableBody.innerHTML = stocks.map((stock, index) => `
      <tr>
        <td>${stock.symbol}</td>
        <td>${stock.companyName}</td>
        <td>${stock.currentPrice}</td>
        <td>${stock.marketCap}</td>
        <td>
          <button onclick="editStock(${index})">Edit</button>
          <button onclick="deleteStock(${index})">Delete</button>
        </td>
      </tr>
    `).join('');
  }
  
  function displayFilterStocks(stocks){
    let tableBody = document.getElementById('stocks') || null;
    if(stocks.length === 0 || tableBody === null){
      return
    }
    console.log(stocks);
    tableBody.innerHTML = stocks.map((stock, index) => `
      <tr>
        <td>${stock.symbol}</td>
        <td>${stock.companyName}</td>
        <td>${stock.currentPrice}</td>
        <td>${stock.marketCap}</td>
        <td>
          <button onclick="editStock(${index})">Edit</button>
          <button onclick="deleteStock(${index})">Delete</button>
        </td>
      </tr>
    `).join('');
  }

  function editStock(index) {
    let stocks = JSON.parse(localStorage.getItem('stocks'));
    const divEdit = document.getElementById('divEdit');
    divEdit.classList.remove('hidden');
    let formEdit = document.getElementById('formEdit');
    formEdit.addEventListener('submit', (event) => {
      event.preventDefault();

      const symbol = document.getElementById('symbolEdit').value;
      const company = document.getElementById('companyEdit').value;
      const currentPrice = parseFloat(document.getElementById('currentPriceEdit').value);
      const marketCap = parseFloat(document.getElementById('marketCapEdit').value);
      
      stocks.splice(index, 1, {
        symbol: symbol,
        companyName: company,
        currentPrice: currentPrice,
        marketCap: marketCap,
      });
      localStorage.setItem('stocks', JSON.stringify(stocks));
      divEdit.classList.add('hidden');
      displayStocks();
    });
  }

  function deleteStock(index) {
    let stocks = JSON.parse(localStorage.getItem('stocks'));
    stocks.splice(index, 1);
    localStorage.setItem('stocks', JSON.stringify(stocks));
    displayStocks();
  }

  function createStock(stock){
    let stocks = JSON.parse(localStorage.getItem('stocks'));
    stocks.push(stock);
    localStorage.setItem('stocks', JSON.stringify(stocks));
    displayStocks();
  }
