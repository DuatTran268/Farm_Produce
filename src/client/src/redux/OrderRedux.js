import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  name: "",
  id: "",
};


const orderFilterReducer = createSlice({
  name: "orderFilter",
  initialState,
  reducers: {
    reset: (state, action) => {
      return initialState;
    },

    updateName: (state, action) => {
      return {
        ...state,
        name: action.payload,
      };
    },
    
    updateId: (state, action) => {
      return {
        ...state,
        id: action.payload,
      };
    },

  },
});


export const {
  reset, 
  updateName,
  updateId,
  
} = orderFilterReducer.actions;

export const orderReducer = orderFilterReducer.reducer;

