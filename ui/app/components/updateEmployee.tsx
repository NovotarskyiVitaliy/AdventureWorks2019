'use client';

import React, { useEffect } from "react";
import { Modal, Form, Input, Radio } from "antd";
import { InitObvject } from "../infrastructure/initObject"

export interface Employee {
    businessEntityId: string;
    firstName: string;
    lastName: string;
    departmentId: string;
}

interface prop {
    visible: any;
    setModalVisible: any;
    employee: Employee
}

export const UpdateEmployee = (props: prop) => {
    const { visible, setModalVisible, employee } = props;


    const [form] = Form.useForm();

    const saveData = async () => {
        const employePost = {
            firstName: form.getFieldValue("firstName"),
            lastName: form.getFieldValue("lastName"),
            businessEntityId: employee.businessEntityId,
        };

        const id = employee.businessEntityId;

        const requestOptions = {
            method: 'POST',
            headers: { 'Content-Type': 'application/json; charset=UTF-8' },
            body: JSON.stringify(employePost)
        };


        console.log(requestOptions);

        const response = await fetch(`${InitObvject.domain}/api/Person/UpdateEmployee`, requestOptions);
        const data = await response.json();
    };

    form.setFieldsValue({
        firstName: employee.firstName,
        lastName: employee.lastName,
    });


    const handleSave = (values) => {
        console.log('Success:', values);
        form
            .validateFields()
            .then(() => {
                saveData();
                setModalVisible(false);
            }
            )
            .catch((info) => {
                console.log("Validate Failed:", info);
            });
    };

    return (
        <Modal
            visible={visible}
            title="Employee detaile"
            okText="Save"
            onCancel={() => {
                setModalVisible(false);
            }}
            onOk={handleSave}
        >
            <Form form={form} layout="vertical">
                <Form.Item
                    label="First Name"
                    name="firstName"
                    rules={[
                        { required: true, message: "Please input the first name of the Employee!" }
                    ]}
                >
                    <Input />
                </Form.Item>

                <Form.Item name="lastName" label="Last Name" rules={[
                    { required: true, message: "Please input the last name of the Employee!" }
                ]}>
                    <Input />
                </Form.Item>

                <Form.Item name="type" label="Type">
                    <Radio.Group>
                        <Radio value="public">Public</Radio>
                        <Radio value="private">Private</Radio>
                    </Radio.Group>
                </Form.Item>
            </Form>
        </Modal>
    );
};
