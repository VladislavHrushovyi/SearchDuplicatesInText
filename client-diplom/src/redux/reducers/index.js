import { combineReducers } from "@reduxjs/toolkit";
import { reportReducer } from "./reportReducer";


export const rootReducer = combineReducers({
    report: reportReducer
})
