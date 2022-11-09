import { combineReducers } from "@reduxjs/toolkit";
import { reportReducer } from "./reportReducer";
import { userReducer } from "./userReducer";


export const rootReducer = combineReducers({
    report: reportReducer,
    user: userReducer
})
