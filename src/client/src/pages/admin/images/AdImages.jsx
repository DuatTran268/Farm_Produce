import React, { useEffect, useState } from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import HeaderBtn from "../../../components/common/HeaderBtn";
import BtnSuccess from "../../../components/common/BtnSuccess";
import { faAdd, faEdit, faTrash } from "@fortawesome/free-solid-svg-icons";
import Loading from "../../../components/common/Loading";
import { Table } from "react-bootstrap";
import { Link, useParams } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import BtnNextPage from "../../../components/common/BtnNextPage";
import Popup from "../../../components/common/Popup";
import { useSnackbar } from "notistack";
import {
  deleteImage,
  getAllImagePagination,
  getFilterComboboxProduct,
} from "../../../api/Image";

const AdImages = () => {
  const [getImage, setgetImage] = useState([]);
  const [refreshData, setRefreshData] = useState(false);
  const { enqueueSnackbar, closeSnackbar } = useSnackbar();
  const [metadata, setMetadata] = useState({});
  const [pageNumber, setPageNumber] = useState(1);

  const [isVisibleLoading, setIsVisibleLoading] = useState(true);

  const [popupMessage, setPopupMessage] = useState("");
  const [popupVisible, setPopupVisible] = useState(false);
  const [IdToDelete, setIdToDelete] = useState(null);

  const [filterProduct, setfilterProduct] = useState({ productList: [] });

  let { id } = useParams,
    p = 1,
    ps = 5;

  function updatePageNumber(inc) {
    setPageNumber((currentVal) => currentVal + inc);
  }

  useEffect(() => {
    document.title = "Quản lý hình ảnh sản phẩm";

    loadDepartment();
    async function loadDepartment() {
      function setData(props) {
        setgetImage(props.items);
        setMetadata(props.metadata);
      }
      getAllImagePagination(ps, pageNumber).then((data) => {
        if (data) {
          setData(data);
          // console.log("Check data catgego",data)
        } else {
          setData([]);
        }
        setIsVisibleLoading(false);
      });
    }

    getFilterComboboxProduct().then((data) => {
      if (data) {
        setfilterProduct({
          productList: data.productList,
        });
      } else {
        setfilterProduct({ productList: [] });
      }
    });
  }, [ps, p, refreshData, pageNumber]);

  const handleDelete = (id) => {
    setIdToDelete(id);
    setPopupMessage("Bạn có muốn xoá Image này?");
    setPopupVisible(true);
  };

  const handleConfirmDelete = async () => {
    const response = await deleteImage(IdToDelete);
    if (response) {
      enqueueSnackbar("Đã xoá thành công", {
        variant: "success",
      });
      setRefreshData((prev) => !prev);
    } else {
      enqueueSnackbar("Xoá thất bại", {
        variant: "error",
      });
    }
    setPopupVisible(false);
  };

  const handleCancelDelete = () => {
    setPopupVisible(false);
  };

  return (
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý Bộ sưu tập ảnh sản phẩm</h3>
      </div>
      <HeaderBtn>
        <BtnSuccess icon={faAdd} slug={"/admin/image/edit"} name="Thêm mới" />
      </HeaderBtn>

      <div className="layout_ad_content">
        {isVisibleLoading ? (
          <Loading />
        ) : (
          <Table responsive bordered>
            <thead>
              <tr>
                <th>Hình ảnh</th>
                <th>Tên hình ảnh</th>
                {/* <th>Thuộc sản phẩm</th> */}
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {getImage.length > 0 ? (
                getImage.map((item, index) => (
                  <tr key={index}>
                    <td>
                      {item.urlImage ? (
                        <img
                          src={`https://localhost:7047/${item.urlImage}`}
                          alt={item.name}
                          height={50}
                        />
                      ) : (
                        <span>Không có hình</span>
                      )}
                    </td>
                    <td>{item.name}</td>
                    {/* <td>{item.productId}</td> */}
                    <td className="text-center">
                      <Link
                        to={`/admin/image/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={() => handleDelete(item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={3}>
                    <h4 className="text-danger text-center">Không tìm thấy</h4>
                  </td>
                </tr>
              )}
            </tbody>
          </Table>
        )}
        <BtnNextPage metadata={metadata} onPageChange={updatePageNumber} />
      </div>
      {popupVisible && (
        <Popup
          message={popupMessage}
          onCancel={handleCancelDelete}
          onConfirm={handleConfirmDelete}
        />
      )}
    </LayoutCommon>
  );
};
export default AdImages;
