import { configureStore } from "@reduxjs/toolkit";
// import { serviceReducer }from './Reducer'
// import { serviceTypeReducer } from "./servicetype/ServiceTypeReducer";
// import { userReducer } from "./user/UserReducer";
// import { supportReducer } from "./support/SupportReducer";

const store = configureStore({
  reducer : {
    // serviceFilter: serviceReducer,
    // serviceTypeFilter: serviceTypeReducer,
    // userFilter: userReducer,
    // supportFilter: supportReducer,
    
  },
});

export default store;