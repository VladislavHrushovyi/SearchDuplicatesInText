import { Button, Checkbox, Col, Form, Input, Row } from "antd";
import axios from "axios";
import React from "react";
import { useDispatch } from "react-redux";
import { useNavigate } from "react-router-dom";
import { Login, SetUserInfo } from "../../redux/actions/userAction";
import { openNotificationWithIcon } from "../../utils/notification";

export const LoginForm = () => {

    const dispatch = useDispatch()
    const navigate = useNavigate()
    const onFinish = (values) => {
        const query = `?email=${values.Email}&password=${values.Password}`
        axios.get("https://localhost:7263/auth/login" + query)
            .then(res => {
                dispatch(Login(res.data))
                axios.get("https://localhost:7263/auth/jwt-isValid", {
                    headers: {
                        "Authorization": "Bearer " + res.data,
                    }
                })
                    .then(res => {
                        const userData = res.data;
                        dispatch(SetUserInfo(userData))
                        navigate("/a")
                    })
                    .catch(function (error) {
                        openNotificationWithIcon("error", { message: "Упс", description: "Щось пішло не так" })
                    })
            })
            .catch(function (error) {
                openNotificationWithIcon('warning', { message: "Помилка авторизації", description: "Невірний логін або пароль" })
            })

    };
    const onFinishFailed = (errorInfo) => {
        console.log('Failed:', errorInfo);
    };

    return (
        <>
            <Row justify="center">
                <Col span={24}>
                    <Form
                        name="basic"
                        labelCol={{
                            span: 6,
                        }}
                        wrapperCol={{
                            span: 12,
                        }}
                        initialValues={{
                            remember: true,
                        }}
                        onFinish={onFinish}
                        onFinishFailed={onFinishFailed}
                        autoComplete="off">
                        <Form.Item label="Електронна пошта"
                            name="Email"
                            rules={[
                                {
                                    type: "email",
                                    message: "Невірний формат електронної пошти"
                                },
                                {
                                    required: true,
                                    message: 'Поле не може бути пустим',
                                },
                            ]}>
                            <Input placeholder="Введіть електронну пошту" />
                        </Form.Item>
                        <Form.Item
                            label="Пароль"
                            name="Password"
                            rules={[
                                {
                                    required: true,
                                    message: 'Поле не може бути пустим',
                                },
                            ]}
                        >
                            <Input.Password placeholder="Введіть пароль" />
                        </Form.Item>
                        <Form.Item
                            name="remember"
                            valuePropName="checked"
                            wrapperCol={{
                                offset: 8,
                                span: 16,
                            }}
                        >
                            <Checkbox>Remember me</Checkbox>
                        </Form.Item>
                        <Form.Item
                            wrapperCol={{
                                offset: 8,
                                span: 16,
                            }}
                        >
                            <Button type="primary" htmlType="submit" size="large">
                                Увійти
                            </Button>
                        </Form.Item>
                    </Form>
                </Col>
            </Row>
        </>
    )
}