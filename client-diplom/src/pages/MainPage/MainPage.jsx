import React from 'react'
import "./MainPage.css"
import { Button, Input, Row, Col, Upload } from 'antd'
import { UploadOutlined } from '@ant-design/icons';
import { useUpload } from '../../hooks/uploadHook';
import { useInputHook } from '../../hooks/useInputHook';
import { postFile } from '../../axios/postFile';
import { CustomSelect } from '../../components/Select/Select';
import { useSelectHook } from '../../hooks/selectHook';

export const MainPage = () => {

    const { TextArea } = Input;
    const uploadHook = useUpload("uploadFile", []);
    const textAreaHook = useInputHook("", "text-user")
    const selectHook = useSelectHook("Метод N-грам", "ngram-check")

    const onClickHandle = () => {
        if (uploadHook.fileList.length !== 0 && uploadHook.fileList[0].status !== "removed") {
            const file = uploadHook.fileList[0];
            postFile(`/CheckDuplicate/file/${selectHook.value}`, file.originFileObj).then(res => {
                console.log(res.data)
            })
        } else if (textAreaHook.value !== "") {
            postFile(`/CheckDuplicate/text/${selectHook.value}`, textAreaHook.value).then(res => {
                console.log(res.data)
            })
        }
    }

    return (
        <>
            <div className="site-layout-content">
                <TextArea rows={10} placeholder="Ваший текст"  {...textAreaHook} className="input-text"/>
                <Row gutter={{ xs: 8, sm: 16, md: 24, lg: 32 }} style={{ marginTop: "25px" }}>
                    <Col className="gutter-row" span={6}>
                        <Upload {...uploadHook} >
                            <Button icon={<UploadOutlined />}>Додати файл</Button>
                        </Upload>
                    </Col>
                    <Col className="gutter-row" span={6}>
                        <CustomSelect hook={selectHook}  />
                    </Col>
                    <Col className="gutter-row" span={6}>
                        <Button onClick={onClickHandle} type="default" block>
                            Перевірити
                        </Button>
                    </Col>
                </Row>
            </div>
        </>
    )
}