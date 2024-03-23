import React from "react";
import LayoutCommon from "../../../components/admin/common/LayoutCommon";
import { Link } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faAdd } from "@fortawesome/free-solid-svg-icons";
import { Table } from "react-bootstrap";

const AdProduct = () => {
  return (
    <LayoutCommon>
      <div className="title py-3 text-danger">
        <h3>Quản lý đăng bài công trình khoa học</h3>
      </div>
      <div className="post-content">
        {/* <PostFilter /> */}
        <div className="d-flex align-items-center justify-content-between">
          <Link className="btn btn-success mb-2" to={`/admin/post/edit`}>
            Thêm mới <FontAwesomeIcon icon={faAdd} />
          </Link>
          <div className="">
            <div className="px-2 text-danger">
              Bài viết chưa phê duyệt:
              <span className="px-1">
                {/* {dashboardItem.countPostNotApprove} */}
              </span>
              dự án
            </div>
            <div className="px-2 text-success">
              Bài viết đã phê duyệt:
              <span className="px-1">
                {/* {dashboardItem.countPostApprove} */}
                </span>
              dự án
            </div>
          </div>
        </div>
        {/* {isVisibleLoading ? (
          <Loading />
        ) : ( */}
          <Table responsive bordered>
            <thead>
              <tr>
                <th className="w-25">Tên bài đăng</th>
                <th className="w-25">Mô tả</th>
                <th>Ngày đăng</th>
                <th>Đăng bởi</th>
                <th>Trạng thái</th>
                <th>Sửa</th>
                <th>Xoá</th>
              </tr>
            </thead>
            <tbody>
              {/* {getPost.length > 0 ? (
                getPost.map((item, index) => (
                  <tr key={index}>
                    <td>{item.title}</td>
                    <td>{item.shortDescription}</td>
                    <td>{format(new Date(item.created), "dd/MM/yyyy")}</td>
                    <td>{item.user?.name}</td>
                    <td>
                      {item.status ? (
                        <div className="text-success">Đã phê duyệt</div>
                      ) : (
                        <div className="text-danger">Chưa phê duyệt</div>
                      )}
                    </td>
                    <td className="text-center">
                      <Link
                        to={`/admin/post/edit/${item.id}`}
                        className="text-warning"
                      >
                        <FontAwesomeIcon icon={faEdit} />
                      </Link>
                    </td>
                    <td className="text-center">
                      <div onClick={(e) => hanldeDeletePost(e, item.id)}>
                        <FontAwesomeIcon icon={faTrash} color="red" />
                      </div>
                    </td>
                  </tr>
                ))
              ) : (
                <tr>
                  <td colSpan={6}>
                    <h4 className="text-danger text-center">
                      Không tìm thấy bài viết nào
                    </h4>
                  </td>
                </tr>
              )} */}
            </tbody>
          </Table>
        {/* )} */}
        {/* <Pager metadata={metadata} onPageChange={updatePageNumber} /> */}
      </div>
    </LayoutCommon>
  );
};
export default AdProduct;
