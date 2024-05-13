import React from "react";
import {
  Text,
  View,
  Page,
  Document,
  StyleSheet,
  Font,
  Image,
} from "@react-pdf/renderer";
import logo from "../../../assets/logo.png";

// Import font
import RobotoRegular from "../../../assets/font/Roboto-Regular.ttf";
import RobotoBold from "../../../assets/font/Roboto-Bold.ttf";
import { Table } from "react-bootstrap";

// Register font
Font.register({
  family: "Roboto",
  fonts: [
    { src: RobotoRegular, fontWeight: "normal" },
    { src: RobotoBold, fontWeight: "bold" },
  ],
});

const styles = StyleSheet.create({
  container: {
    padding: 10,
    fontFamily: "Roboto",
  },
  logoflex: {
    flexDirection: "row",
    justifyContent: "space-between",
    alignItems: "center",
    marginBottom: 10,
  },
  signed: {
    flexDirection: "row",
    justifyContent: "space-around",
    alignItems: "center",
    marginBottom: 10,
    marginTop: 20,
  },
  logo: {
    width: 60,
    height: 60,
  },
  text: {
    fontSize: 10,
    marginBottom: 10,
  },
  textPayment: {
    fontSize: 13,
    marginTop: 10,
  },
  textH3: {
    textAlign: "center",
    fontSize: 16,
    padding: 10,
  },

  productInfoHeader: {
    fontSize: 10,
    flexDirection: "row",
    justifyContent: "flex-start",
    marginBottom: 5,
    borderBottomWidth: 1,
    borderBottomColor: "#ccc",
    borderBottomStyle: "solid",
  },
  productInfo: {
    fontSize: 10,
    flexDirection: "row",
    justifyContent: "flex-start",
    marginBottom: 5,
    borderBottomWidth: 1,
    borderBottomColor: "#ccc",
    borderBottomStyle: "solid",
  },
  cellHeader: {
    fontWeight: "bold",
    width: "100%",
  },
  cellText: {
    paddingBottom: 5,
    width: "100%",
  },
  cellHeader30: {
    fontWeight: "bold",
    width: "30%",
  },
  cellText30: {
    paddingBottom: 5,
    width: "30%",
  },
});

const Invoice = ({ orderData }) => {
  if (!orderData) {
    return <div>Loading...</div>;
  }

  return (
    <Document>
      <Page size="A5">
        <View style={styles.container}>
          <View style={styles.logoflex}>
            <Image src={logo} style={styles.logo} />
            <Text style={styles.textH3}>Hung Duat Farm</Text>
          </View>
          <Text style={styles.textH3}>Thông tin khách hàng</Text>

          <Text style={styles.text}>Tên khách hàng: {orderData.userName}</Text>
          <Text style={styles.text}>Địa chỉ: {orderData.address}</Text>
          <Text style={styles.text}>Ngày đặt hàng: {orderData.dateOrder}</Text>
          <Text style={styles.text}>
            Số điện thoại: {orderData.phoneNumber}
          </Text>
          <Text style={styles.text}>
            Phương thức thanh toán: {orderData.paymentMethodName}
          </Text>

          <Text style={styles.textH3}>Thông tin sản phẩm</Text>

          {orderData.orderItems && orderData.orderItems.length > 0 ? (
            <View>
              <View style={styles.productInfoHeader}>
                <Text style={styles.cellHeader30}>Id</Text>
                <Text style={styles.cellHeader}>Sản phẩm</Text>
                <Text style={styles.cellHeader30}>Đơn giá</Text>
                <Text style={styles.cellHeader30}>Số lượng</Text>
                <Text style={styles.cellHeader30}>Thành tiền</Text>
              </View>

              {orderData.orderItems.map((itemOrder, index) => (
                <View key={index} style={styles.productInfo}>
                  <Text style={styles.cellText30}>{itemOrder.id}</Text>
                  <Text style={styles.cellText}>{itemOrder.productName}</Text>
                  <Text style={styles.cellText30}>{itemOrder.price}₫</Text>
                  <Text style={styles.cellText30}>{itemOrder.quantity}</Text>
                  <Text style={styles.cellText30}>
                    {itemOrder.price * itemOrder.quantity}₫
                  </Text>
                </View>
              ))}
              <Text style={styles.textPayment}>
                Tổng tiền phải thanh toán: {orderData.totalPrice} VNĐ
              </Text>
              <View style={styles.signed}>
                <Text style={styles.text}>Khách hàng</Text>
                <Text style={styles.text}>Người bán</Text>
              </View>
            </View>
          ) : (
            <Text style={styles.textH3}>Không có thông tin sản phẩm nào</Text>
          )}
        </View>
      </Page>
    </Document>
  );
};

export default Invoice;
