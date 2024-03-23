import { configureStore } from "@reduxjs/toolkit";
import { unitReducer } from "./Unit";


const store = configureStore({
  reducer : {
    unitFilter: unitReducer,
  
    
  },
});

export default store;