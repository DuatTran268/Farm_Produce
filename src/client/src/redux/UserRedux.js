import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  name: "",
};


const userFilterReducer = createSlice({
  name: "userFilter",
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
  
} = userFilterReducer.actions;

export const userReducer = userFilterReducer.reducer;

