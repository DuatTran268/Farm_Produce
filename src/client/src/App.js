// import logo from './logo.svg';
// import './App.css';

import { Provider } from "react-redux";
import Routers from "./router/Routers";
import store from "./redux/Store";
import { CartProvider } from "react-use-cart";

function App() {
  return (
    <div className="app">
      <Provider store={store}>
        <CartProvider>
          <Routers />
        </CartProvider>
      </Provider>
    </div>
  );
}

export default App;
