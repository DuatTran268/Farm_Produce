// import logo from './logo.svg';
// import './App.css';

import { Provider } from "react-redux";
import store from "./redux/Store";
import { CartProvider } from "react-use-cart";
import { SnackbarProvider } from "notistack";
import AdminPanel from "./pages/admin/Admin";
import { Admin, AdminRouter, ListGuesser, Resource } from "react-admin";
import dataProvider from "./apiProvider";
import RouterUser from "./router/RouterUser";
import RouterAdmin from "./router/RouterAdmin";

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
            <RouterUser />
          </CartProvider>
          <RouterAdmin/>
        </SnackbarProvider>
      </Provider>
    </div>
  );
}

export default App;
