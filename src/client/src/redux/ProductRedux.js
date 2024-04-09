import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  name: "",
};


const productFilerReducer = createSlice({
  name: "productFilter",
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


  },
});


export const {
  reset, 
  updateName,
  
} = productFilerReducer.actions;

export const productReduce = productFilerReducer.reducer;


