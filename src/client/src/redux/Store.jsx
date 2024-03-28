import { configureStore } from "@reduxjs/toolkit";
import { unitReducer } from "./UnitRedux";
import { commentReduce } from "./CommentRedux";


const store = configureStore({
  reducer : {
    unitFilter: unitReducer,
    commentFilter: commentReduce,
  
    
  },
});

export default store;