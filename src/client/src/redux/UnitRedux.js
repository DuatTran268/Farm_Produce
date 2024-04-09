import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  name: "",
};


const unitFilterReducer = createSlice({
  name: "unitFilter",
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
  
} = unitFilterReducer.actions;

export const unitReducer = unitFilterReducer.reducer;

