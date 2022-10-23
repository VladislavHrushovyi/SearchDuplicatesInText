import { Select } from 'antd';
import React from 'react';

const options = [
    {
        value: "ngram-check",
        name: "Метод N-грам"
    },
    {
        value: "shingle-check",
        name: "Метод шинглів"
    },
    {
        value: "exp-check",
        name: "Експерементальний метод"
    }
]

export const CustomSelect = ({ hook }) => {

    const { Option } = Select;

    return (
    <>
        <Select
            defaultValue={hook.value}
            style={{
                width: 200,
            }}
            onChange={hook.onChange}
        >
            {options.map((opt,index) => <Option key={index} value={opt.value}>{opt.name}</Option>)}
        </Select>
    </>)
}