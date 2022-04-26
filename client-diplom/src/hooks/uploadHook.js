import { useState } from "react"

export const useUpload = (name, list) => {
    const [file, setFile] = useState(list);

    return {
        name: name,
        customRequest: ({ file, onSuccess }) => {
            onSuccess("ok")
        },
        onChange: (response) => {
            setFile([response.file])
            console.log(response)
        },
        beforeUpload: file => {
            const reader = new FileReader();
    
            reader.onload = e => {
                console.log(e.target.result);
            };
            reader.readAsText(file);
            
            return false;
        },
        fileList: file,        
    }
}