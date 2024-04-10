// import logo from './logo.svg';
// import './App.css';

import { Provider } from "react-redux";
import Routers from "./router/Routers";
import store from "./redux/Store";
import { CartProvider } from "react-use-cart";
import { SnackbarProvider } from "notistack";

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
            <Routers />
          </CartProvider>
        </SnackbarProvider>
      </Provider>
    </div>
  );
}

export default App;
