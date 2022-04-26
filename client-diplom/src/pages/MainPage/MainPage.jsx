import React from 'react'
import "./MainPage.css"
import { Button, Input, Row, Col, Upload } from 'antd'
import { UploadOutlined } from '@ant-design/icons';
import { useUpload } from '../../hooks/uploadHook';

export const MainPage = () => {

    const { TextArea } = Input;
    const uploadHook = useUpload("uploadFile", []);
    
    const onClickHandle = () => {
        console.log(uploadHook.fileList[0])
    }

    return (
        <>
            <div className="site-layout-content">
                <TextArea rows={10} placeholder="Ваший текст" maxLength={6} />
                <Row gutter={{ xs: 8, sm: 16, md: 24, lg: 32 }} style={{ marginTop: "25px" }}>
                    <Col className="gutter-row" span={6}>
                        <Upload {...uploadHook}>
                            <Button icon={<UploadOutlined />}>Додати файл</Button>
                        </Upload>
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