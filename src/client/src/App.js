// import logo from './logo.svg';
// import './App.css';

import { Provider } from "react-redux";
import store from "./redux/Store";
import { CartProvider } from "react-use-cart";
import { SnackbarProvider } from "notistack";
import "./index.css"
import Router from "./router/Router";

function App() {
  return (
    <div className="app">

      <Provider store={store}>
        <SnackbarProvider
          sx={{ height: "100%" }}
          anchorOrigin={{
            vertical: "top",
            horizontal: "center",
          }}
        >
          <CartProvider>
            <Router />
          </CartProvider>
        </SnackbarProvider>
      </Provider>
    </div>
  );
}

export default App;
