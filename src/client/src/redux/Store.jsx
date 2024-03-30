import { configureStore } from "@reduxjs/toolkit";
import { unitReducer } from "./UnitRedux";
import { commentReduce } from "./CommentRedux";
import { productReduce } from "./ProductRedux";


const store = configureStore({
  reducer : {
    unitFilter: unitReducer,
    commentFilter: commentReduce,
    productFilter: productReduce,
  
    
  },
});

export default store;