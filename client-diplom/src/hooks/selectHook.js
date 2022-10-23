import { useState } from "react"

export const useSelectHook = (name, init) => {
    const [value, setValue] = useState(init)
    console.log(value)
    return {
        value: value,
        name: name,
        onChange: (v) => {
            setValue(v)
        }
    }
}