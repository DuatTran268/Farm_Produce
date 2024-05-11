import { createSlice } from "@reduxjs/toolkit";

const initialState = {
  name: "",

};


const commentFilerReducer = createSlice({
  name: "commentFilter",
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
  
} = commentFilerReducer.actions;

export const commentReduce = commentFilerReducer.reducer;

