import { configureStore } from "@reduxjs/toolkit";
import { unitReducer } from "./UnitRedux";
import { commentReduce } from "./CommentRedux";
import { productReduce } from "./ProductRedux";

import authReducer from "./Account"
import { userReducer } from "./UserRedux";

const store = configureStore({
  reducer : {
    unitFilter: unitReducer,
    commentFilter: commentReduce,
    productFilter: productReduce,
    auth: authReducer,
    userFilter: userReducer,
    
  },
});

export default store;