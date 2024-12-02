import React, { useState } from "react";
import SearchForm from "../components/SearchForm";

const SearchPage = () => {
    const [searchResult, setSearchResult] = useState(null);
    const [hasSearched, setHasSearched] = useState(false);

    const handleSearch = (result) => {
        if (result && result.keyword && result.url) {
            setSearchResult(result);
        } else {
            console.error("Invalid search result format:", result);
            setSearchResult(null);
        }
        setHasSearched(true);
    };

    return (
        <div className="search-page">
            <h1>Perform a Search</h1>
            <SearchForm onSearch={handleSearch} />
            {hasSearched && !searchResult && (
                <div className="no-results-message">
                    <p>No results found. Please try a different search.</p>
                </div>
            )}
            {searchResult && (
                <div className="results">
                    <h2>Search Results:</h2>
                    <table className="results-table">
                        <thead>
                            <tr>
                                <th>Keyword</th>
                                <th>URL</th>
                                <th>Positions</th>
                                <th>Search Engine</th>
                                <th>Timestamp</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>{searchResult.keyword}</td>
                                <td>{searchResult.url}</td>
                                <td>{searchResult.positions}</td>
                                <td>{searchResult.searchEngine}</td>
                                <td>{new Date(searchResult.timestamp).toLocaleString()}</td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            )}
        </div>
    );
};

export default SearchPage;